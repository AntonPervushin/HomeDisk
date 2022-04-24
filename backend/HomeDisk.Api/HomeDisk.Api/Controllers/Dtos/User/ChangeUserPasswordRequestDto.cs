namespace HomeDisk.Api.Controllers.Dtos.User
{
    public sealed class ChangeUserPasswordRequestDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
