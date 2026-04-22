import { KnockoutResult } from "../types/worldcup";
import { Bracket } from "../components/Bracket";

type KnockoutProps = {
  knockout: KnockoutResult | null;
};

export function Knockout({ knockout }: KnockoutProps) {
  if (!knockout) {
    return null;
  }

  const stages = [
    { title: "Oitavas de Final", matches: knockout.roundOf16 },
    { title: "Quartas de Final", matches: knockout.quarterFinals },
    { title: "Semifinal", matches: knockout.semiFinals },
    { title: "Final", matches: [knockout.final] },
  ];

  return (
    <section className="page-section">
      <div className="page-section__header">
        <div>
          <p className="eyebrow-ui">Segunda etapa</p>
          <h2>Mata-Mata</h2>
        </div>
        <span className="badge">Chave oficial</span>
      </div>
      <div className="knockout-grid-ui">
        {stages.map((stage) => (
          <Bracket key={stage.title} title={stage.title} matches={stage.matches} />
        ))}
      </div>
    </section>
  );
}
