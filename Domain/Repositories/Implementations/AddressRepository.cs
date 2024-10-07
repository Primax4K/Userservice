namespace Domain.Repositories.Implementations;

public class AddressRepository(UserServiceDbContext context) : ARepository<Address>(context), IAddressRepository { }