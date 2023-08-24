var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
static double CalculateApparentTemperature(double temperatureCelsius, double relativeHumidity)
{
    // Constants for the heat index calculation
    double c1 = -8.78469475556;
    double c2 = 1.61139411;
    double c3 = 2.33854883889;
    double c4 = -0.14611605;
    double c5 = -0.012308094;
    double c6 = -0.0164248277778;
    double c7 = 0.002211732;
    double c8 = 0.00072546;
    double c9 = -0.000003582;

    double Tc = temperatureCelsius;
    double R = relativeHumidity;

    // Calculate the apparent temperature (heat index)
    double AT = c1 + c2 * Tc + c3 * R + c4 * Tc * R + c5 * Tc * Tc + c6 * R * R + c7 * Tc * Tc * R + c8 * Tc * R * R + c9 * Tc * Tc * R * R;

    return Math.Round(AT, 2);
}

app.MapPost("/", (Data data) => 
        Results.Ok(CalculateApparentTemperature(data.Temparature, data.RelativeHumidity))
    );
app.Run();
public record Data(double Temparature, double RelativeHumidity);
