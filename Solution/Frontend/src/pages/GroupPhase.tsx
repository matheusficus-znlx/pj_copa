import { GroupStageResult, Group } from "../types/worldcup";
import { GroupCard } from "../components/GroupCard";
import { MatchRow } from "../components/MatchRow";

type GroupPhaseProps = {
  groups: Group[];
  groupStage: GroupStageResult[];
};

export function GroupPhase({ groups, groupStage }: GroupPhaseProps) {
  return (
    <section className="page-section">
      <div className="page-section__header">
        <div>
          <p className="eyebrow-ui">Primeira etapa</p>
          <h2>Fase de Grupos</h2>
        </div>
        <span className="badge">8 grupos, 3 rodadas</span>
      </div>

      <div className="groups-grid-ui">
        {groups.map((group) => (
          <GroupCard key={group.label} group={group} />
        ))}
      </div>

      <div className="group-matches-grid">
        {groupStage.map((group) => (
          <section className="panel" key={group.groupLabel}>
            <div className="page-section__header">
              <div>
                <p className="group-card-ui__eyebrow">Calendário</p>
                <h3>Grupo {group.groupLabel}</h3>
              </div>
              <span className="badge">{group.matches.length} partidas</span>
            </div>
            {group.matches.map((match) => (
              <MatchRow key={match.stage} match={match} />
            ))}
          </section>
        ))}
      </div>
    </section>
  );
}
