import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Domain } from "../types/Domain";
import { deleteADomain, fetchAllDomains, updateADomain } from "../services/api";

const DomainsPage: React.FC = () => {
  const [domains, setDomains] = useState<Domain[]>([]);
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [editingDomain, setEditingDomain] = useState<Domain | null>(null);

  useEffect(() => {
    fetchAllDomains().then((response: ApiResponse<Domain>) => {
      setDomains(response.data.data);
    });
  }, []);

  const handleDeleteDomain = async (domainId: number) => {
    await deleteADomain(domainId);
    setDomains(domains.filter(domain => domain.domainId !== domainId));
  };

  const handleEditDomain = (domain: Domain) => {
    setEditingDomain(domain);
    setIsEditing(true);
  };

  const handleUpdateDomain = async () => {
    if (editingDomain) {
      await updateADomain(editingDomain.domainId, editingDomain);  
      setDomains(
        domains.map((domain) =>
          domain.domainId === editingDomain.domainId ? editingDomain : domain
        )
      );
      setIsEditing(false); 
      setEditingDomain(null); 
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
