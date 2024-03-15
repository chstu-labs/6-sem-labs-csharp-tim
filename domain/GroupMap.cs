using FluentNHibernate.Mapping;

namespace Lab1;

public class GroupMap : ClassMap<Group>
{
    public GroupMap()
    {
        Table("Groups");
        Id(x => x.Id).GeneratedBy.Native();
        Map(x => x.GroupName);

        Map(x => x.CuratorName);
        Map(x => x.HeadmanName);
        HasMany(x => x.StudentList)
        .Cascade.All()
        .KeyColumn("GroupId");
    }
}

