namespace Course_project {
    public interface IUser {
        string Password { get; set; }
        string SecretAnswer { get; set; }
        string SecretQuestion { get; set; }
        string Username { get; set; }
    }
}