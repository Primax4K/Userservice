namespace Model.Configurations;

public class UserServiceDbContext : DbContext {
	public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options) { }

	public DbSet<Address> Addresses { get; set; } = null!;
	public DbSet<Country> Countries { get; set; } = null!;
	public DbSet<State> States { get; set; } = null!;
	public DbSet<Application> Applications { get; set; } = null!;
	public DbSet<LoginAttempt> LoginAttempts { get; set; } = null!;
	public DbSet<Session> Sessions { get; set; } = null!;
	public DbSet<ApplicationRole> ApplicationRoles { get; set; } = null!;
	public DbSet<RegisteredUserRole> RegisteredUserRoles { get; set; } = null!;
	public DbSet<Role> Roles { get; set; } = null!;
	public DbSet<Gender> Genders { get; set; } = null!;
	public DbSet<LoginUserData> LoginUserData { get; set; } = null!;
	public DbSet<RegisteredUser> RegisteredUsers { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder) {
		builder.Entity<RegisteredUser>()
			.HasOne(r => r.LoginUser)
			.WithOne()
			.HasForeignKey<RegisteredUser>(r => r.Id);

		builder.Entity<User>()
			.HasOne(u => u.RegisteredUser)
			.WithOne()
			.HasForeignKey<User>(u => u.Id);
		builder.Entity<User>()
			.HasOne(u => u.Address)
			.WithMany()
			.HasForeignKey(u => u.AddressId);
		builder.Entity<User>()
			.HasOne(u => u.Gender)
			.WithMany()
			.HasForeignKey(u => u.GenderId);

		builder.Entity<State>()
			.HasOne(s => s.Country)
			.WithMany()
			.HasForeignKey(s => s.CountryId);

		builder.Entity<Address>()
			.HasOne(a => a.State)
			.WithMany()
			.HasForeignKey(a => a.StateId);


		builder.Entity<LoginAttempt>()
			.HasOne(l => l.RegisteredUser)
			.WithMany()
			.HasForeignKey(l => l.RegisteredUserId);
		builder.Entity<LoginAttempt>()
			.HasOne(l => l.Application)
			.WithMany()
			.HasForeignKey(l => l.ApplicationId);

		builder.Entity<ApplicationRole>()
			.HasKey(a => new { a.ApplicationId, a.RoleId });
		builder.Entity<ApplicationRole>()
			.HasOne(a => a.Application)
			.WithMany()
			.HasForeignKey(a => a.ApplicationId);
		builder.Entity<ApplicationRole>()
			.HasOne(a => a.Role)
			.WithMany()
			.HasForeignKey(a => a.RoleId);

		builder.Entity<RegisteredUserRole>()
			.HasKey(r => new { r.RegisteredUserId, r.RoleId, r.ApplicationId });
		builder.Entity<RegisteredUserRole>()
			.HasOne(r => r.ApplicationRole)
			.WithMany()
			.HasForeignKey(r => new { r.ApplicationId, r.RoleId });
		builder.Entity<RegisteredUserRole>()
			.HasOne(r => r.RegisteredUser)
			.WithMany(r => r.RegisteredUserRoles)
			.HasForeignKey(r => r.RegisteredUserId);

		builder.Entity<Session>()
			.HasOne(s => s.RegisteredUser)
			.WithMany()
			.HasForeignKey(s => s.UserId);

		builder.Entity<RegisteredUser>().Navigation(r => r.RegisteredUserRoles).AutoInclude();
		builder.Entity<RegisteredUserRole>().Navigation(r => r.ApplicationRole).AutoInclude();
		builder.Entity<ApplicationRole>().Navigation(r => r.Role).AutoInclude();

		builder.Entity<Role>().Property(r => r.Name).HasConversion<string>();

		builder.Entity<LoginUserData>().HasIndex(l => l.Email).IsUnique();
		builder.Entity<Role>().HasIndex(l => l.Name).IsUnique();

		//seeding
		builder.Entity<Role>().HasData(new Role { Id = 1, Name = ERole.Patient },
			new Role { Id = 2, Name = ERole.Assistant }, new Role { Id = 3, Name = ERole.Taxconsultant }, new Role { Id = 4, Name = ERole.Administrator });

		builder.Entity<Country>().HasData(new Country() { Id = 1, Name = "Österreich" });
		builder.Entity<State>().HasData(
			new State() { Id = 1, CountryId = 1, Name = "Niederösterreich" },
			new State() { Id = 2, CountryId = 1, Name = "Wien" },
			new State() { Id = 3, CountryId = 1, Name = "Burgenland" },
			new State() { Id = 4, CountryId = 1, Name = "Oberösterreich" },
			new State() { Id = 5, CountryId = 1, Name = "Steiermark" },
			new State() { Id = 6, CountryId = 1, Name = "Kärnten" },
			new State() { Id = 7, CountryId = 1, Name = "Salzburg" },
			new State() { Id = 8, CountryId = 1, Name = "Tirol" },
			new State() { Id = 9, CountryId = 1, Name = "Vorarlberg" }
		);

		builder.Entity<Gender>().HasData(
			new Gender() { Id = 1, GenderName = "Männlich" },
			new Gender() { Id = 2, GenderName = "Weiblich" }
		);


		//testuser
		builder.Entity<Application>().HasData(new Application() {
			Id = 99,
			Name = "TestApp",
			Port = 0,
			Url = "-",
			Key = "abc123"
		});
		builder.Entity<Application>().HasData(new Application() {
			Id = 100,
			Name = "App2",
			Port = 0,
			Url = "-",
			Key = "key1234"
		});

		builder.Entity<LoginUserData>().HasData(new LoginUserData() {
			Id = 99,
			Email = "user@test.com",
			CreatedAt = DateTime.Now,
			FirstName = "Test",
			LastName = "User",
		});

		builder.Entity<RegisteredUser>().HasData(new RegisteredUser() {
			Id = 99,
			IsLocked = false,
			PasswordHash = "$2a$08$m70kGY6l/LtFejFyvVcMieVx/kyLEZy/pL42OkQ3EZLtQlddb0N0K" //passwort = password123
		});

		builder.Entity<ApplicationRole>().HasData(
			new ApplicationRole() {
				ApplicationId = 99,
				RoleId = 1
			}, new ApplicationRole() {
				ApplicationId = 99,
				RoleId = 2
			}, new ApplicationRole() {
				ApplicationId = 99,
				RoleId = 3
			}, new ApplicationRole() {
				ApplicationId = 99,
				RoleId = 4
			},
			new ApplicationRole() {
				ApplicationId = 100,
				RoleId = 1
			}, new ApplicationRole() {
				ApplicationId = 100,
				RoleId = 2
			}, new ApplicationRole() {
				ApplicationId = 100,
				RoleId = 3
			}, new ApplicationRole() {
				ApplicationId = 100,
				RoleId = 4
			}
		);

		builder.Entity<RegisteredUserRole>().HasData(
			new RegisteredUserRole() {
				ApplicationId = 99,
				RoleId = 4,
				RegisteredUserId = 99
			},
			new RegisteredUserRole() {
				ApplicationId = 100,
				RoleId = 4,
				RegisteredUserId = 99
			}
		);
	}
}