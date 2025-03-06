## Construire ma première API .NET core.

**contrainte :** Utiliser .NET core 8

**l'objectif :**

Créer une mini application de type admin, permet de gérer des utilisateurs et des publications.

## Cahier des charges : 

**Utilisateurs :**

- Ajouter un user
- Modifier un user
- Delete un user
- Récupérer un utilisateur sur base de son id.
- Récupérer une liste d'utilisateurs en utilsant de la pagination.

**Posts :**
- Créer un post
- Update un post
- Delete un post
- Récupérer un post sur base de son ID
- Récupérer une liste de poste en utilisant de la pagination
- Récupérer les posts d'un utilisateur.

=> Particularité : Un post peut avoir un ou plusieurs domaines liés. Ex : un post peut etre lié a de l'informatique et la sécurité.

Comment je mets en place une logique DB capable de : 

- Gérer des users
- Gérer des posts
- Gérer un système de domaines liés à des posts.

---

## Architecture .NET : 

- Models
- Controllers
- Services
- DTOS
- Repository
- ORM => Utilisation d'entity framwork core
- DB  => SQL SERVER.

---

## Développement : 

- Structurer le plan DB
- Mise en place d'une API .NET core restfull utilisant la clean architecture comme modèle de développement.
- Mise en place d'une application REACT minimale pour une gestion utilisateur prononcée.

----

## Inspiration : 

https://github.com/Yekuuun/fast-aspnet