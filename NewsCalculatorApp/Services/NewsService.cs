using NewsCalculatorApp.DTOs;
using Range = NewsCalculatorApp.Models.Range;

namespace NewsCalculatorApp.Services;

public class NewsService
{
    private readonly List<Models.ScoreRange> _bodyTemperatureScores =
    [
        new(31, 35, 3),
        new(35, 36, 1),
        new(36, 38, 0),
        new(38, 39, 1),
        new(39, 42, 2),
    ];

    private readonly List<Models.ScoreRange> _heartRateScores =
    [
        new(25, 40, 3),
        new(40, 50, 1),
        new(50, 90, 0),
        new(90, 110, 1),
        new(110, 130, 2),
        new(130, 220, 3),
    ];

    private readonly List<Models.ScoreRange> _respiratoryRateScores =
    [
        new(3, 8, 3),
        new(8, 11, 1),
        new(11, 20, 0),
        new(20, 24, 2),
        new(24, 60, 3),
    ];
    
    private readonly ScoreLimits _scoreLimits;

    public int GetNewsScore(NewsInput newsInput)
    {
        byte tempScore = _bodyTemperatureScores.Find(x => x.Min <= newsInput.BodyTemperature && x.Max >= newsInput.BodyTemperature)?.Score ??
            throw new ArgumentOutOfRangeException(nameof(newsInput), "Temperature is out of range");

        byte heartRateScore = _heartRateScores.Find(x => x.Min <= newsInput.HeartRate && x.Max >= newsInput.HeartRate)?.Score ??
            throw new ArgumentOutOfRangeException(nameof(newsInput), "Heart rate is out of range");

        byte respiratoryRateScore = _respiratoryRateScores.Find(x => x.Min <= newsInput.RespiratoryRate && x.Max >= newsInput.RespiratoryRate)?.Score ??
            throw new ArgumentOutOfRangeException(nameof(newsInput), "Respiratory rate is out of range");

        return tempScore + heartRateScore + respiratoryRateScore;
    }
    
    public ScoreLimits GetScoreLimits() => _scoreLimits;
    
    public NewsService()
    {
        _scoreLimits = new ScoreLimits(
            new Range(_bodyTemperatureScores[0].Min, _bodyTemperatureScores[^1].Max),
            new Range(_heartRateScores[0].Min, _heartRateScores[^1].Max),
            new Range(_respiratoryRateScores[0].Min, _respiratoryRateScores[^1].Max)
        );
    }
}
