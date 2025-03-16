namespace SchoolManagement.Core.Features.Students.Queries.Responses
{
    public class GetAllStudentsPaginamtedResponse
    {

        public GetAllStudentsPaginamtedResponse(int studID, string address, string name, string dName)
        {
            StudID = studID;
            Address = address;
            Name = name;
            DeparmentName = dName;
        }

        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public String? DeparmentName { get; set; }
    }
}
