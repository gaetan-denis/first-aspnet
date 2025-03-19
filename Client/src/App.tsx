
import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const[data, setdata] = useState<any>(null);


  useEffect(()=>{
    fetch("http://localhost:5086/api/v1/users",{
      method : "get",
      headers: {
        'Content-Type' : 'application/json',
      },
    })
    .then((response)=>{
      if(!response.ok){
        console.error("Une erreur est survenue lors de la récupération des données");
      }else{
        return response.json();
      }
    })

    .then((data) =>{
      setdata(data.data);
    })
    
    
  },[])
  return (
    <>
     <h1></h1>
     <pre>{JSON.stringify(data, null, 2)}</pre>
    </>
  )
}

export default App
