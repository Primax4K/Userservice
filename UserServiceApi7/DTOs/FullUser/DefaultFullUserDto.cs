namespace UserServiceApi7.DTOs.FullUser;

public class DefaultFullUserDto {
	public DefaultLoginUserDto? LoginUser { get; set; }
	public DefaultRegisteredUserDto? RegisteredUser { get; set; }
	public DefaultUserDto? User { get; set; }
	public DefaultAddressDto? Address { get; set; }
	public ERole? Role { get; set; }
}