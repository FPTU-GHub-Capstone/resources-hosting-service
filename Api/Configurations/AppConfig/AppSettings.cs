namespace WebApiLayer.Configurations.AppConfig;

public class AppSettings
{
    public DbConfig ConnectionStrings { get; set; }
    public string IdpUrl { get; set; }
    public JWTOptions JWTOptions { get; set; }
}
