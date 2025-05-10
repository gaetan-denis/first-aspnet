import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Domain } from "../types/Domain";
import { deleteADomain, fetchAllDomains, addAdomain, updateADomain } from "../services/api";

const DomainsPage: React.FC = () => {
  const [domains, setDomains] = useState<Domain[]>([]);
  const [newDomain, setNewDomain] = useState<Domain>({
    domainId: 0,
    name: "",
  });
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [editingDomain, setEditingDomain] = useState<Domain | null>(null);

  useEffect(() => {
    fetchAllDomains().then((response: ApiResponse<Domain>) => {
      setDomains(response.data.data);
    });
  }, []);

  const handleDeleteDomain = async (domainId: number) => {
    await deleteADomain(domainId);
    setDomains(domains.filter((domain) => domain.domainId !== domainId));
  };

  const handleAddDomain = async (e: React.FormEvent) => {
    e.preventDefault();

    if (newDomain.name.trim() === "") {
      alert("Le nom du domaine est requis.");
      return;
    }

    const response = await addAdomain(newDomain); 
    if (response?.data) {

      /* 
      * Si je laisse ceci souligné le code fonctionne parfaitement. 
      * Si je le remplace par `setDomains([...domains, ...response.data]);`L'erreur disparait mais le code plante en console.
      */

      setDomains([...domains, response.data]); 
      setNewDomain({
        domainId: 0,
        name: "",
      }); 
    } else {
      alert("Erreur lors de l'ajout du domaine.");
    }
  };

  const handleEditDomain = (domain: Domain) => {
    setEditingDomain(domain);
    setIsEditing(true); 
  };

  const handleUpdateDomain = async () => {
    if (editingDomain) {
      
      const updatedDomainData = await updateADomain(editingDomain.domainId, editingDomain);
      if (updatedDomainData) {
        setDomains(
          domains.map((domain) =>
            domain.domainId === editingDomain.domainId ? editingDomain : domain
          )
        );
        setIsEditing(false); 
        setEditingDomain(null);
      } else {
        alert("Erreur lors de la mise à jour du domaine.");
      }
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (editingDomain) {
      setEditingDomain({
        ...editingDomain,
        [e.target.name]: e.target.value,
      });
    }
  };

  return (
    <div>
      <h2>Listes des domaines</h2>
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
      {isEditing && editingDomain ? (
        <div>
          <h3>Modifier le domaine</h3>
          <input
            type="text"
            name="name"
            value={editingDomain.name}
            onChange={handleChange}
          />
          <button onClick={handleUpdateDomain}>Sauvegarder</button>
          <button onClick={() => setIsEditing(false)}>Annuler</button>
        </div>
      ) : (
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
                  <button onClick={() => handleEditDomain(domain)}>
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
      )}
    </div>
  );
};

export default DomainsPage;
