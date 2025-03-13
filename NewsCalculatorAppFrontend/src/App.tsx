import axios, { isAxiosError } from "axios";
import Button from "./component/Button";
import { useEffect, useState } from "react";

type ScoreLimits = {
  bodyTemperature: {
    min: number;
    max: number;
  };
  heartRate: {
    min: number;
    max: number;
  };
  respiratoryRate: {
    min: number;
    max: number;
  };
};

function App() {
  const [newsScore, setNewsScore] = useState<number | null>(null);
  const [scoreLimits, setScoreLimits] = useState<ScoreLimits | null>(null);

  async function submitForm(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const formData = new FormData(event.target as HTMLFormElement);
    const bodyTemperature = formData.get("bodyTemperature");
    const heartRate = formData.get("heartRate");
    const respiratoryRate = formData.get("respiratoryRate");

    try {
      const { data } = await axios.post("http://localhost:4000/api/news", {
        bodyTemperature,
        heartRate,
        respiratoryRate,
      });

      setNewsScore(data.score);
    } catch (error: unknown) {
      if (isAxiosError(error)) {
        console.log(error.response?.data);
      }
    }
  }

  useEffect(() => {
    async function fetchScoreLimits() {
      try {
        const { data } = await axios.get("http://localhost:4000/api/news/limits");
        setScoreLimits(data);
      } catch (error) {
        console.error(error);
      }
    }

    fetchScoreLimits();
  }, []);

  return (
    <div className="w-full h-full bg-white flex items-center justify-center">
      <div className="w-full max-w-[404px] flex flex-col gap-[40px]">
        <h1 className="text-h1">NEWS Score Calculator</h1>

        <form className="flex flex-col gap-[40px]" onSubmit={submitForm}>
          <div className="flex flex-col gap-[12px]">
            <div className="flex flex-col gap-[8px]">
              <h2 className="text-h2">Body temperature</h2>
              <label className="text-small">Degrees celcius</label>
            </div>

            <input
              className="news-input"
              name="bodyTemperature"
              type="number"
              step="0.1"
              required
              min={scoreLimits ? scoreLimits.bodyTemperature.min : undefined}
              max={scoreLimits ? scoreLimits.bodyTemperature.max : undefined}
            />
          </div>

          <div className="flex flex-col gap-[12px]">
            <div className="flex flex-col gap-[8px]">
              <h2 className="text-h2">Heartrate</h2>
              <label className="text-small">Beats per minute</label>
            </div>

            <input
              className="news-input"
              name="heartRate"
              type="number"
              required
              min={scoreLimits ? scoreLimits.heartRate.min : undefined}
              max={scoreLimits ? scoreLimits.heartRate.max : undefined}
            />
          </div>

          <div className="flex flex-col gap-[12px]">
            <div className="flex flex-col gap-[8px]">
              <h2 className="text-h2">Respiratory rate</h2>
              <label className="text-small">Breaths per minute</label>
            </div>

            <input
              className="news-input"
              name="respiratoryRate"
              type="number"
              required
              min={scoreLimits ? scoreLimits.respiratoryRate.min : undefined}
              max={scoreLimits ? scoreLimits.respiratoryRate.max : undefined}
            />
          </div>

          <div className="flex gap-[24px]">
            <Button
              label="Calculate NEWS score"
              variant="primary"
              type="submit"
            />
            <Button
              label="Reset form"
              variant="secondary"
              type="reset"
              onClick={() => setNewsScore(null)}
            />
          </div>
        </form>

        {newsScore !== null && (
          <div className="flex rounded-[10px] bg-[#FAF6FF] p-[16px] border border-[#7424DA66]">
            <p className="text-[20px] font-[400]">
              NEWS score: <span className="font-[600]">{newsScore}</span>
            </p>
          </div>
        )}
      </div>
    </div>
  );
}

export default App;
