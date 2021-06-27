Projeto TesteSoft
=====================
O Projeto TesteSoft é um projeto de teste para mostrar de forma simples e pratica como podem ser desenvolvidas aplicações/serviços de APIs/Rest em .NET Core.
O Projeto TesteSoft mostra também como é possivel configurar/orquestrar a instalação das alicações em containers usando Docker e Docker Compose.

## Instruções para usar:
 - Você precisará do Docker e docker-compose instalado na maquina.
     
- ### Para criar os Containers das duas APIs deste Exemplo:
 - #### Com o Docker em execução.
    * Em qualquer terminal de sua preferência, acessar a pasta Raiz dos Projetos: "TesteSoft".
    * Executar os seguintes comandos, onde será definido como será criado as imagens dos containers:
     - * ```docker-compose build```
    * Em seguida, executar o seguinte comando:
     - * ```docker-compose up -d```
    * Após estes passos, estarão criados os containers onde estarão rodando os 2 Serviços/APIs.
    
- ### Executando a Aplicação de que permite Consultar a Taxa de Juros:
  - Testando a API de Link do Código Fonte através do seu navegador pelo Swagger(Recomendado) usando o link: http://localhost:5000/swagger/index.html ou de qualquer programa como insominia, postman e outros usando da seguinte url: http://localhost:5000/calculaJuros.
- ### Executando a Aplicação de que permite efetuar o Cálculo e Exibição do Link de onde está hospedado estes projetos de exemplo:
  - Testando a API de Cálculo através do seu navegador pelo Swagger(Recomendado) usando o link: http://localhost:7000/swagger/index.html ou de qualquer programa como insominia, postman e outros usando da seguinte url: http://localhost:7000/calculajuros.
  - Testando a API de Link do Código Fonte através do seu navegador pelo Swagger(Recomendado) usando o link: http://localhost:7000/swagger/index.html ou de qualquer programa como insominia, postman e outros usando da seguinte url: http://localhost:7000/showmethecode.




Além disso, você pode executar o Projeto TesteSoft no Visual Studio Code (Windows, Linux ou MacOS).

Para saber mais sobre como configurar seu ambiente, visite o Guia de [download](https://dotnet.microsoft.com/download) do Microsoft .NET

- ### Executando a Aplicação API Taxa de Juros:
  - No visual Studio, com a solução do projeto "TesteSoft.Taxa" aberto, efetuar um Build.
  - No Visual Studio, com a solução do projeto "TesteSoft.Taxa" aberto, selecionar para executar na opção de self-host(TesteSoft.Taxa).
  - Executar o projeto.
- ### Executando a Aplicação API Cálculo de Juros:
  - No visual Studio, com a solução do projeto "TesteSoft.Calculo" aberto, efetuar um Build.
  - No Visual Studio, com a solução do projeto "TesteSoft.Calculo" aberto, selecionar para executar na opção de self-host(TesteSoft.Calculo).

## Tecnologias implementadas:

- ASP.NET Core 3.1 (with .NET Core 3.1)
- .NET Core Native DI
- Swagger UI

## Arquitetura:

- Arquitetura simples, separando apenas por pastas com questões de separação de responsabilidades devido a simplicidade do exemplo, Código SÓLIDO e Limpo.

