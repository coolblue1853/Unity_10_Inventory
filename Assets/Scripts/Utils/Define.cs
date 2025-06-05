
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
    // ���� �̸�, ���� �Ұ� ����
    private static readonly Dictionary<Job, (string Name, string Description)> jobInfo = new Dictionary<Job, (string, string)>()
    {
        { Job.Slave, ("�ڵ��뿹", "�ڵ��� �뿹�� ���� 10��¥�� �Ǵ� �ӽ��Դϴ�. ���õ� ����ϸ� ���Ƽ� ġŲ�� ��ų���� �𸥴ٴ� ������ ����� Ű�� �ֳ׿�.") },
    };

    public static string GetName(Job job)
    {
        if (jobInfo.TryGetValue(job, out var value))
            return value.Name;
        return "�̸� ���� ����";
    }

    public static string GetDescription(Job job)
    {
        if (jobInfo.TryGetValue(job, out var value))
            return value.Description;
        return "������ ���� ����";
    }
}
