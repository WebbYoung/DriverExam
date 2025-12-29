using Driver.DTO;

namespace Driver.IService
{
	public interface IUserService
	{
		Task<UserDTO?> GetByPhoneNumberAsync(string phoneNumber);
		Task<bool> CheckLoginAsync(string phoneNumber, string password);
		Task AddUserAsync(string phoneNumber, string password);
		Task<UserDTO?> GetByIdAsync(long id);
	}
}
