#include <windows.h>
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <sstream>
#include <iomanip>
#include <map>
#include <algorithm>

struct SensorRecord
{
    std::string sensor_id;
    std::string timestamp;
    double value;
    double normalized;
    std::string status;
};

struct ThreadContext
{
    int threadIndex;
    size_t startIndex;
    size_t endIndex;
    const std::vector<SensorRecord>* inputData;
    std::vector<SensorRecord>* outputData;
    double minVal;
    double maxVal;
    DWORD errorCode;
};

DWORD WINAPI WorkerThreadProc(LPVOID lpParam)
{
    ThreadContext* ctx = (ThreadContext*)lpParam;
    if (!ctx) return (DWORD)-1;

    try
    {
        for (size_t i = ctx->startIndex; i < ctx->endIndex; ++i)
        {
            const auto& src = (*ctx->inputData)[i];
            auto& dest = (*ctx->outputData)[i];

            dest.sensor_id = src.sensor_id;
            dest.timestamp = src.timestamp;
            dest.value = src.value;

            dest.normalized = (src.value - ctx->minVal) / (ctx->maxVal - ctx->minVal);

            if (src.value < ctx->minVal || src.value > ctx->maxVal)
            {
                dest.status = "OUT";
            }
            else
            {
                dest.status = "OK";
            }
        }
        ctx->errorCode = 0;
    }
    catch (...)
    {
        ctx->errorCode = (DWORD)-1;
    }

    return 0;
}

void GenerateSensorsCsv(const std::string& filename)
{
    std::ofstream outFile(filename);

    if (!outFile.is_open())
    {
        std::cerr << "Error creating file " << filename << std::endl;
        return;
    }

    outFile << "sensor_id;timestamp;value\n";

    for (int i = 0; i < 100000; ++i)
    {
        std::string s_id = "S" + std::to_string(i % 100);
        std::string ts = "t" + std::to_string(i);
        double val = (i % 151) - 25;
        outFile << s_id << ";" << ts << ";" << val << "\n";
    }
    outFile.close();
}

int main(int argc, char* argv[])
{
    Sleep(20000);

    SetConsoleOutputCP(65001);
    SetConsoleCP(65001);

    int K = 4;
    double minLimit = 0.0;
    double maxLimit = 100.0;
    std::string inputFilename = "sensors.csv";

    if (argc > 1) K = std::stoi(argv[1]);
    if (argc > 2) minLimit = std::stod(argv[2]);
    if (argc > 3) maxLimit = std::stod(argv[3]);

    if (maxLimit <= minLimit)
    {
        std::cerr << "Error: max limit must be greater than min limit!" << std::endl;
        return 1;
    }

    std::cout << "MyWorker started. PID: " << GetCurrentProcessId() << ", Threads (K): " << K << std::endl;

    {
        std::ifstream checkFile(inputFilename);

        if (!checkFile.is_open())
        {
            std::cout << "File " << inputFilename << " not found. Generating 100,000 rows..." << std::endl;
            GenerateSensorsCsv(inputFilename);
        }
        else
        {
            checkFile.close();
        }
    }

    std::vector<SensorRecord> records;
    std::ifstream inFile(inputFilename);

    if (!inFile.is_open())
    {
        std::cerr << "Error opening file " << inputFilename << std::endl;
        return 1;
    }

    std::string line;
    std::getline(inFile, line);
    size_t lineNum = 1;

    while (std::getline(inFile, line))
    {
        lineNum++;
        std::stringstream ss(line);
        std::string s_id, ts, valStr;

        if (std::getline(ss, s_id, ';') && std::getline(ss, ts, ';') && std::getline(ss, valStr, ';'))
        {
            try
            {
                double val = std::stod(valStr);
                records.push_back({ s_id, ts, val, 0.0, "" });
            }
            catch (...)
            {
                std::cerr << "Error at line " << lineNum << ": invalid number format (" << valStr << ")." << std::endl;
                inFile.close();
                return 1;
            }
        }
        else
        {
            std::cerr << "Error at line " << lineNum << ": invalid fields count." << std::endl;
            inFile.close();
            return 1;
        }
    }
    inFile.close();

    size_t totalRecords = records.size();
    std::cout << "Successfully read records: " << totalRecords << std::endl;

    std::vector<SensorRecord> outputRecords(totalRecords);

    if (K <= 0) K = 1;
    if ((size_t)K > totalRecords) K = (int)totalRecords;

    std::vector<HANDLE> threadHandles;
    std::vector<ThreadContext> contexts(K);
    size_t chunkSize = totalRecords / K;
    threadHandles.reserve(K);

    for (int i = 0; i < K; ++i)
    {
        contexts[i].threadIndex = i;
        contexts[i].startIndex = i * chunkSize;
        contexts[i].endIndex = (i == K - 1) ? totalRecords : (i + 1) * chunkSize;
        contexts[i].inputData = &records;
        contexts[i].outputData = &outputRecords;
        contexts[i].minVal = minLimit;
        contexts[i].maxVal = maxLimit;
        contexts[i].errorCode = 0;

        HANDLE hThread = CreateThread(NULL, 0, WorkerThreadProc, &contexts[i], CREATE_SUSPENDED, NULL);

        if (hThread == NULL)
        {
            std::cerr << "Error creating thread " << i << std::endl;
            return 1;
        }
        threadHandles.push_back(hThread);
    }

    std::cout << "Created " << K << " threads (suspended). Resuming..." << std::endl;

    for (size_t i = 0; i < threadHandles.size(); ++i)
    {
        ResumeThread(threadHandles[i]);
    }

    if (K <= 32)
    {
        WaitForMultipleObjects((DWORD)threadHandles.size(), threadHandles.data(), TRUE, INFINITE);
    }
    else
    {
        for (size_t i = 0; i < threadHandles.size(); ++i)
        {
            WaitForSingleObject(threadHandles[i], INFINITE);
        }
    }

    for (size_t i = 0; i < threadHandles.size(); ++i)
    {
        CloseHandle(threadHandles[i]);
    }

    std::ofstream normFile("normalized.csv");
    normFile << "sensor_id;timestamp;value;normalized;status\n";

    for (const auto& r : outputRecords)
    {
        normFile << r.sensor_id << ";" << r.timestamp << ";" << r.value << ";"
            << std::fixed << std::setprecision(4) << r.normalized << ";" << r.status << "\n";
    }
    normFile.close();

    struct StatAgg
    {
        int count = 0;
        double min = 1e9;
        double max = -1e9;
        double sum = 0.0;
        int out_count = 0;
    };

    std::map<std::string, StatAgg> statsMap;

    for (const auto& r : outputRecords)
    {
        auto& st = statsMap[r.sensor_id];
        st.count++;
        if (r.value < st.min) st.min = r.value;
        if (r.value > st.max) st.max = r.value;
        st.sum += r.value;
        if (r.status == "OUT") st.out_count++;
    }

    std::ofstream statsFile("stats.csv");
    statsFile << "sensor_id;count;min;max;mean;out_count\n";

    for (const auto& pair : statsMap)
    {
        double mean = (pair.second.count > 0) ? (pair.second.sum / pair.second.count) : 0.0;
        statsFile << pair.first << ";"
            << pair.second.count << ";"
            << pair.second.min << ";"
            << pair.second.max << ";"
            << std::fixed << std::setprecision(2) << mean << ";"
            << pair.second.out_count << "\n";
    }
    statsFile.close();

    std::cout << "Task completed successfully! Files normalized.csv and stats.csv created." << std::endl;
    return 0;
}