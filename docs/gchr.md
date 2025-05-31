## Prerequisites

- **GitHub CLI** installed and authenticated:  
    ```sh
    gh auth login
    ```
- **Docker** installed and running.

- **Logged into GHCR** (GitHub Container Registry):

    ```sh
    echo "<GITHUB_PAT>" | docker login ghcr.io -u <USERNAME> --password-stdin
    ```
    > Replace `<GITHUB_PAT>` with a GitHub Personal Access Token (PAT) that has `write:packages`, `read:packages`, and `delete:packages` scopes.


## Build and Push Docker Image (Manually)

```sh
cd src
docker build -f VA.API/Dockerfile -t ghcr.io/amitpnk/vertical-slice-architecture/va-api:latest .
docker push ghcr.io/amitpnk/vertical-slice-architecture/va-api:latest
```