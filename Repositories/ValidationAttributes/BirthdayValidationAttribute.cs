using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ValidationAttributes
{
    public class BirthdayValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue && dateValue >= DateTime.Parse("2007-01-01"))
            {
                return new ValidationResult("Value for birthday < 01-01-2007");
            }
            return ValidationResult.Success;
        }

    }
}
/*if (StartDate.HasValue && EndDate.HasValue)
{
    filters.Add($"CreatedDate ge {StartDate.Value:yyyy-MM-dd} and CreatedDate le {EndDate.Value:yyyy-MM-dd}");
}*/
/*Lọc theo một giá trị cụ thể (Equals)
/odata/PackagingProcesses?$filter=PackagingProcessId eq 1

Lọc theo nhiều điều kiện (And, Or)
/odata/PackagingProcesses?$filter=PackagingCost gt 100 and QualityCheckStatus eq 'Approved'
/odata/PackagingProcesses?$filter=HandlerName eq 'John' or PackagingCost lt 50

Lọc với điều kiện phủ định (Not Equals)
/odata/PackagingProcesses?$filter=QualityCheckStatus ne 'Rejected'

Lọc theo chuỗi ký tự (Contains, Startswith, Endswith)
/odata/PackagingProcesses?$filter=contains(HandlerName, 'John')
/odata/PackagingProcesses?$filter=startswith(HandlerName, 'A')
/odata/PackagingProcesses?$filter=endswith(HandlerName, 'son')

 Lọc theo ngày tháng (Greater Than, Less Than)
/odata/PackagingProcesses?$filter=PackagingDate gt 2023-01-01
/odata/PackagingProcesses?$filter=PackagingDate lt 2023-12-31

Lọc theo khoảng giá trị (Between)
/odata/PackagingProcesses?$filter=PackagingCost ge 50 and PackagingCost le 200

Sắp xếp dữ liệu (Order By)
/odata/PackagingProcesses?$orderby=PackagingDate asc
/odata/PackagingProcesses?$orderby=PackagingCost desc

Giới hạn số lượng kết quả trả về (Top)
/odata/PackagingProcesses?$top=10

Phân trang dữ liệu (Skip và Top)
/odata/PackagingProcesses?$skip=10&$top=5

Chọn những cột cụ thể (Select)
/odata/PackagingProcesses?$select=PackagingProcessId, HandlerName, PackagingCost

*/