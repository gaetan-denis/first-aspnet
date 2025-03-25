import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Domain } from "../types/Domain";
import { deleteADomain, fetchAllDomains, addAdomain } from "../services/api";

const DomainsPage: React.FC = () => {
  const [domains, setDomains] = useState<Domain[]>([]);
  const [newDomain, setNewDomain] = useState<Domain>({
    domainId : 0,
    name: "" ,
    });

  useEffect(() => {
    fetchAllDomains().then((response: ApiResponse<Domain>) => {
      setDomains(response.data.data);
    });
  }, []);

  const handleDeleteDomain = async (domainId: number) => {
    await deleteADomain(domainId);
    setDomains(domains.filter(domain => domain.domainId !== domainId));
  };

  const handleAddDomain = async (e: React.FormEvent) => {
    e.preventDefault();

    if (newDomain.name.trim() === "") {
      alert("Le nom du domaine est requis.");
      return;
    }

    const response = await addAdomain(newDomain); // Appel à la fonction addAdomain
    if (response?.data) {
     
      /* 
      * Si je laisse ceci souligné le code fonctionne parfaitement. 
      * Si je le remplace par `setDomains([...domains, ...response.data]);`L'erreur disparait mais le code plante en console.
      */

      setDomains([...domains, response.data]); 
      setNewDomain({ 
        domainId : 0,
        name: "" }); // Réinitialiser le formulaire
    } else {
      alert("Erreur lors de l'ajout du domaine.");
    }
  };

  return (
    <div>
      <h2>Listes des domaines</h2>

      {/* Formulaire pour ajouter un domaine */}
      <form onSubmit={handleAddDomain}>
        <label htmlFor="domainName">Nom du domaine :</label>
        <input
          type="text"
          id="domainName"
          value={newDomain.name}
          onChange={(e) => setNewDomain({ ...newDomain, name: e.target.value })}
          required
        />
        <button type="submit">Ajouter</button>
      </form>

      <table>
        <thead>
          <tr>
            <th>Nom</th>
            <th>Modifier</th>
            <th>Supprimer</th>
          </tr>
        </thead>
        <tbody>
          {domains.map((domain) => (
            <tr key={domain.domainId}>
              <td>{domain.name}</td>
              <td>
                <button>
                  <i className="fa-solid fa-pen"></i>
                </button>
              </td>
              <td>
                <button onClick={() => handleDeleteDomain(domain.domainId)}>
                  <i className="fa-solid fa-trash"></i>
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default DomainsPage;
