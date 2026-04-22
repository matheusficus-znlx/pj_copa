import { useState } from "react";
import { motion } from "framer-motion";
import { fetchTeams, simulateCup } from "../services/api";
import { GroupPhase } from "./GroupPhase";
import { Knockout } from "./Knockout";
import { FinalResult } from "../components/FinalResult";
import { SimulationResponse, Team } from "../types/worldcup";

export function Home() {
  const [gitUser, setGitUser] = useState("");
  const [teams, setTeams] = useState<Team[]>([]);
  const [simulation, setSimulation] = useState<SimulationResponse | null>(null);
  const [status, setStatus] = useState("Aguardando");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const canUseApi = /^[a-zA-Z0-9._-]{3,}$/.test(gitUser);
  const champion =
    simulation && simulation.knockout.final
      ? simulation.knockout.final.winnerId === simulation.knockout.final.homeTeam.id
        ? simulation.knockout.final.homeTeam.name
        : simulation.knockout.final.awayTeam.name
      : null;
  const sectionMotion = {
    initial: { opacity: 0, y: 18 },
    animate: { opacity: 1, y: 0 },
    transition: { duration: 0.35 },
  };

  async function handleLoadTeams() {
    if (!canUseApi) {
      setError("Informe um git-user válido.");
      return;
    }

    setLoading(true);
    setError("");
    setStatus("Carregando seleções...");

    try {
      const response = await fetchTeams(gitUser);
      setTeams(response);
      setStatus(`${response.length} seleções carregadas.`);
    } catch (requestError) {
      setError("Não foi possível carregar as seleções pelo backend.");
      setStatus("Erro");
    } finally {
      setLoading(false);
    }
  }

  async function handleSimulate(sendFinalResult = true) {
    if (!canUseApi) {
      setError("Informe um git-user válido.");
      return;
    }

    setLoading(true);
    setError("");
    setStatus("Simulando Copa...");

    try {
      const response = await simulateCup(gitUser, sendFinalResult);
      setTeams(response.teams);
      setSimulation(response);
      setStatus(sendFinalResult ? "Simulação concluída e final enviada." : "Nova simulação gerada.");
    } catch (_requestError) {
      setError("Não foi possível executar a simulação pelo backend.");
      setStatus("Erro");
    } finally {
      setLoading(false);
    }
  }

  return (
    <main className="page">
      {simulation && champion ? (
        <motion.section
          className="champion-banner"
          initial={{ opacity: 0, y: -10 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.35 }}
        >
          <div className="champion-banner__copy">
            <p className="eyebrow-ui">Campeão da simulação</p>
            <h2>{champion}</h2>
            <span className="badge badge--highlight">Final concluída</span>
          </div>
          <div className="champion-banner__score">
            <span>{simulation.knockout.final.homeGoals}</span>
            <small>x</small>
            <span>{simulation.knockout.final.awayGoals}</span>
          </div>
        </motion.section>
      ) : null}

      <motion.section
        className="panel hero-panel"
        initial={{ opacity: 0, y: 16 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.35 }}
      >
        <div className="hero-copy">
          <p className="eyebrow-ui">Dashboard MVC</p>
          <h1>Simulador da Copa do Mundo 2026</h1>
          <p className="hero-copy__text">
            Frontend em React + TypeScript consumindo o backend ASP.NET Core, que por sua vez
            orquestra a lógica da Copa e a integração com a API externa.
          </p>

          <div className="hero-metrics">
            <article className="hero-metric">
              <span>Seleções</span>
              <strong>{teams.length || 32}</strong>
            </article>
            <article className="hero-metric">
              <span>Status</span>
              <strong>{loading ? "Processando" : status}</strong>
            </article>
            <article className="hero-metric">
              <span>Fluxo</span>
              <strong>Grupos + Mata-mata</strong>
            </article>
          </div>
        </div>

        <div className="control-card">
          <div className="control-card__header">
            <div>
              <p className="eyebrow-ui">Integração</p>
              <h2>Painel de Controle</h2>
            </div>
            <span className="badge badge--highlight">git-user obrigatório</span>
          </div>

          <label className="input-label">
            Git User
            <input
              value={gitUser}
              onChange={(event) => setGitUser(event.target.value)}
              placeholder="Seu usuário"
            />
          </label>

          <div className="actions-ui">
            <button disabled={loading || !canUseApi} onClick={handleLoadTeams}>Carregar seleções</button>
            <button disabled={loading || !canUseApi} onClick={() => handleSimulate(true)}>Simular e enviar final</button>
            <button disabled={loading || !canUseApi} onClick={() => handleSimulate(false)}>Nova simulação</button>
          </div>

          {loading ? (
            <div className="loading-card">
              <span className="loading-orb" aria-hidden="true"></span>
              <div>
                <strong>Processando simulação</strong>
                <p>{status}</p>
              </div>
            </div>
          ) : null}

          <div className="status-grid">
            <div className="status-box">
              <strong>Status</strong>
              <span>{loading ? "Processando..." : status}</span>
            </div>
            <div className="status-box">
              <strong>Seleções</strong>
              <span>{teams.length}</span>
            </div>
          </div>
          {error ? <div className="error-box">{error}</div> : null}
        </div>
      </motion.section>

      {simulation ? (
        <>
          <motion.div {...sectionMotion} transition={{ duration: 0.3, delay: 0.05 }}>
            <GroupPhase groups={simulation.groups} groupStage={simulation.groupStage} />
          </motion.div>
          <motion.div {...sectionMotion} transition={{ duration: 0.3, delay: 0.12 }}>
            <Knockout knockout={simulation.knockout} />
          </motion.div>
          <motion.div {...sectionMotion} transition={{ duration: 0.3, delay: 0.18 }}>
            <FinalResult result={simulation.finalResult} finalMatch={simulation.knockout.final} />
          </motion.div>
        </>
      ) : (
        <motion.section className="panel empty-panel" {...sectionMotion}>
          <div className="empty-panel__crest" aria-hidden="true">
            <span className="empty-panel__dot"></span>
          </div>
          <p className="eyebrow-ui">Pronto para começar</p>
          <h2>Nenhuma simulação executada</h2>
          <p>
            Carregue as seleções, informe seu <strong>git-user</strong> e gere a jornada completa
            da Copa com grupos, mata-mata e resultado final.
          </p>
        </motion.section>
      )}
    </main>
  );
}
