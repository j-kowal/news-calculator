using System.ComponentModel.DataAnnotations;
namespace NewsCalculatorApp.DTOs;

public record NewsInput(
    [Required, Range(31, 42)] float BodyTemperature,
    [Required, Range(25, 220)] byte HeartRate,
    [Required, Range(3, 60)] byte RespiratoryRate
);

