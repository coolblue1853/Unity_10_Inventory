
using static Define;
using System.Collections.Generic;

public class Define
{
   public enum Button
    {
        Main = 0,
        Status = 1,
        Inventory = 2,
    }

    public enum Job
    {
        Slave = 0,
    }

}
public struct JobInfo
{
    public Job JobType;
    public string Name;
    public string Description;

    public JobInfo(Job jobType)
    {
        JobType = jobType;
        Name = JobData.GetName(jobType);
        Description = JobData.GetDescription(jobType);
    }
}

public static class JobData
{
    // 직업 이름, 직업 소개 순서
    private static readonly Dictionary<Job, (string Name, string Description)> jobInfo = new Dictionary<Job, (string, string)>()
    {
        { Job.Slave, ("코딩노예", "코딩의 노예가 된지 10년짜리 되는 머슴입니다. 오늘도 밤샐일만 남아서 치킨을 시킬지도 모른다는 생각에 배민을 키고 있네요.") },
    };

    public static string GetName(Job job)
    {
        if (jobInfo.TryGetValue(job, out var value))
            return value.Name;
        return "이름 없는 직업";
    }

    public static string GetDescription(Job job)
    {
        if (jobInfo.TryGetValue(job, out var value))
            return value.Description;
        return "설명이 없는 직업";
    }
}
