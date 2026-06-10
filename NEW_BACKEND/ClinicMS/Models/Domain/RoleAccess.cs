namespace ClinicMS.Models.Domain
{
    public class RoleAccess
    {
        public int    RoleAccessId  { get; set; }
        public string CompCode      { get; set; }
        public string UserRoleId    { get; set; }
        public string ModuleId      { get; set; }
        public string SubModuleId   { get; set; }
        public string MenuId        { get; set; }
        public bool   ViewAction    { get; set; }
        public bool   InsertAction  { get; set; }
        public bool   UpdateAction  { get; set; }
        public bool   DeleteAction  { get; set; }
    }
}
