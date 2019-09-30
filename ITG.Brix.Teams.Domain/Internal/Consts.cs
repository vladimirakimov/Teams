namespace ITG.Brix.Teams.Domain.Internal
{
    internal static class Consts
    {
        internal static class DomainExceptionMessage
        {
            public const string NameShouldNotBeNull = "Name shouldn't be null.";

            public const string NameFieldShouldNotBeEmpty = "Name field shouldn't be empty.";
            public const string DescriptionFieldShouldNotBeEmpty = "Description field shouldn't be empty.";
            public const string LayoutFieldShouldNotBeDefaultGuid = "Layout field shouldn't be default guid.";
            public const string MemberFieldShouldNotBeDefaultGuid = "Member field shouldn't be default guid.";
        }
    }
}
