using EmbroiderData;
using System;

namespace EmbroiderManagementSystem.Helpers
{
    internal static class GenderExtensions
    {
        public static string GetString(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female: return "Female";
                case Gender.Male: return "Male";
                case Gender.None: return "None";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
    }
}
