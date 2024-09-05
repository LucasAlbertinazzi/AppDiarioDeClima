# Diário de Clima

**Diário de Clima** é um aplicativo mobile desenvolvido com Xamarin Forms que permite ao usuário consultar a previsão do tempo para diversas cidades, visualizar gráficos de variação de temperatura e armazenar as últimas consultas realizadas.

## Funcionalidades
- Consulta de previsão do tempo em diferentes cidades usando a [OpenWeatherMap API](https://openweathermap.org/api).
- Exibição de gráficos simples com variação de temperatura usando o [Microcharts](https://github.com/dotnet-ad/Microcharts). **Nota:** Os métodos que mostram a variação de temperatura do dia não estão conectados à API devido a um serviço pago. Em vez disso, foi realizada uma randomização das temperaturas com base nas temperaturas máxima e mínima fornecidas pela API.
- Diferenciação visual entre Android e iOS.
- Navegação simples entre páginas.
- Animação de carregamento durante o consumo da API.
- **Ícones personalizados** para Android e iOS.
- **Autenticação** para fazer login na aplicação.

## Tecnologias Utilizadas
- Xamarin Forms
- OpenWeatherMap API
- Microcharts (exibição de gráficos)
- MVVM (Model-View-ViewModel)
- **Amazon RDS** com **PostgreSQL** para o banco de dados

## Banco de Dados
O banco de dados PostgreSQL está hospedado no Amazon RDS e pode ser acessado com as seguintes credenciais:
```Host=projeto-dev.crcsaggk48qa.us-east-1.rds.amazonaws.com Database=postgres Username=postgres Password=postgres```


## Instalação

1. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/diario-de-clima.git
    ```

2. Abra o projeto no Visual Studio.

3. Instale as dependências necessárias via NuGet:
    - Microcharts.Forms
    - Newtonsoft.Json (para manipulação de JSON)

4. Obtenha uma chave de API gratuita da [OpenWeatherMap](https://openweathermap.org/api) e insira-a no arquivo `WeatherService.cs`.

5. Compile e execute o projeto em um dispositivo ou emulador Android/iOS.

## Execução da API Local

Para executar a API localmente, siga estas etapas:

1. Adicione o seguinte código ao arquivo `launchSettings.json` da sua API:
    ```json
    "API": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "principal",
      "applicationUrl": "http://192.168.3.4:8989",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
    ```
   - Substitua `http://192.168.3.4:8989` pelo IP e porta da sua máquina.
   - Certifique-se de liberar essa porta como saída no firewall do Windows.

2. Execute o projeto da API, com o nome do executável `API`.

3. No aplicativo, dentro da classe `Global`, modifique o IP e a porta abaixo para o IP e porta configurados na API:
    ```csharp
    namespace AppDiarioDeClima.Classes
    {
        public static class Global
        {
            public static string UrlApi = "http://192.168.3.4:8989/api";
            public static int CodUser;
            public static string NomeUser;
        }
    }
    ```

4. Compile e construa o projeto em um dispositivo conectado à mesma rede da sua API. A aplicação funcionará normalmente.

## Estrutura do Projeto
- **Services**: Lida com a requisição de dados da API.
- **Models**: Contém as classes que representam os dados da previsão do tempo.
- **Views**: Páginas de interface, como lista de cidades e detalhes da previsão.
- **ViewModels**: Lógica da interface e comunicação entre a View e os Models.
