using System.ComponentModel.DataAnnotations;

namespace ComponentWebApi.Model.Identity
{
    /// <summary>
    ///     登录信息
    /// </summary>
    public class LoginRequestDTO
    {
        [Required] public string Username { get; set; }


        [Required] public string Password { get; set; }
    }
}