using System.ComponentModel;

namespace Jobfinder.Domain.Enums;

public enum MilitaryServiceStatus
{
    [Description("مشمول")]
    NotServedYet = 0,
    [Description("انجام شده")]
    Completed = 1,
    [Description("معافیت دائم")]
    Exempted = 2,
    [Description("معافیت تحصیلی")]
    EducationalExemption = 3,
    [Description("در حال خدمت")]
    Ongoing = 4
} 