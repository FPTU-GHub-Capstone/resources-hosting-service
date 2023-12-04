namespace WebApiLayer.Configurations.AppConfig;
public class JWTOptions {
    public string Secret { get; set; }
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
}
