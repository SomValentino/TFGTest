namespace TFG.Unit.Tests.Setup;

public class TestConfiguration {
    public static string GetJWTSecret () {
        return "test_key_1234465758";
    }

    public static double GetJWTExpry () {
        return 3600;
    }

    public static string BaseUrl () {
        return "http://localhost:5001";
    }
}