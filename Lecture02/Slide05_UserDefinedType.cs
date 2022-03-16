namespace Lecture02;


public record GroupNameRecord(int Course, int Group, int Specialization);

public class GroupName
{
    public int Course { get; }
    public int Group { get; }
    public int Specialization { get; }

    public GroupName(int course, int group, int specialization)
    {
        Course = course;
        Group = group;
        Specialization = specialization;
    }

    public static bool operator ==(GroupName left, GroupName right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GroupName left, GroupName right)
    {
        return !(left == right);
    }

    protected bool Equals(GroupName other)
    {
        return Course == other.Course
               && Group == other.Group
               && Specialization == other.Specialization;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GroupName)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Course, Group, Specialization);
    }
}