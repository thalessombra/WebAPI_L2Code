# 📦 API Organizadora de Pedidos

Esta é uma API desenvolvida em .NET que recebe uma lista de pedidos com produtos e suas dimensões, 
e organiza esses produtos em caixas previamente definidas, respeitando os limites de tamanho. 
A resposta é retornada em formato JSON com a disposição dos produtos em cada caixa.

---

## 🚀 Funcionalidades

- Recebe pedidos com múltiplos produtos e suas dimensões
- Agrupa produtos em caixas de tamanhos fixos (Caixa 1, 2 e 3)
- Retorna a organização de produtos por pedido
- Validação de espaço restante nas caixas
- Autenticação via JWT
- Persistência de dados com SQL Server

---

## 🧰 Tecnologias Utilizadas

- ASP.NET Core
- C#
- SQL Server
- Entity Framework Core
- JWT (JSON Web Tokens)
- Docker-compose

---

## ✅ Pré-requisitos

- Docker Compose instalado

---

## ⚙️ Como executar

1. Clone este repositório:

bash
git clone https://github.com/thalessombra/WebAPI_L2Code
cd EmbalagemLojaApi 
docker-compose up

---
## Como testar
1. Após iniciar a aplicação, acesse a documentação interativa da API pelo Swagger no link:

http://localhost:5071/swagger/index.html

Lá você pode testar os endpoints diretamente pelo navegador.
