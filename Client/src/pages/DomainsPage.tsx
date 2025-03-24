import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Domain } from "../types/Domain";
import { deleteADomain, fetchAllDomains} from "../services/api";

const DomainsPage: React.FC = () => {
  const [domains, setDomains] = useState<Domain[]>([]);

  useEffect(() => {
    fetchAllDomains().then((response: ApiResponse<Domain>) => {
      console.log(response);
      setDomains(response.data.data);
    });
  }, []);

   const handleDeleteDomain = async (domainId : number) => {
          await deleteADomain(domainId)
          setDomains(domains.filter(domain => domain.domainId !== domainId));
      }
  
  return (
    <div>
      <h2>Listes des domaines</h2>
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
            
              <td><button><i className="fa-solid fa-pen"></i></button></td>
              <td><button onClick={() => handleDeleteDomain(domain.domainId)}><i className="fa-solid fa-trash"></i></button></td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default DomainsPage;
