
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

    public string Description;

    public JobInfo(Job jobType)
    {
        JobType = jobType;
        Description = JobDescriptions.GetDescription(JobType);
    }
}
public static class JobDescriptions
{
    private static readonly Dictionary<Job, string> descriptions = new Dictionary<Job, string>()
    {
        { Job.Slave, "근접 전투에 특화된 전사입니다." },
    };

    public static string GetDescription(Job job)
    {
        if (descriptions.TryGetValue(job, out var description))
            return description;
        return "설명이 없는 직업입니다.";
    }
}
