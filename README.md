# Diário de Clima

**Diário de Clima** é um aplicativo mobile desenvolvido com Xamarin Forms que permite ao usuário consultar a previsão do tempo para diversas cidades, visualizar gráficos de variação de temperatura e armazenar as últimas consultas realizadas. O projeto é parte de um teste técnico de nível Júnior.

## Funcionalidades
- Consulta de previsão do tempo em diferentes cidades usando a [OpenWeatherMap API](https://openweathermap.org/api).
- Armazenamento local das últimas 5 consultas (SQLite ou Xamarin Preferences).
- Exibição de gráficos simples com variação de temperatura usando o [Microcharts](https://github.com/dotnet-ad/Microcharts).
- Diferenciação visual entre Android e iOS.
- Navegação simples entre páginas.
- Animação de carregamento durante o consumo da API.

## Tecnologias Utilizadas
- Xamarin Forms
- OpenWeatherMap API
- SQLite ou Xamarin Preferences (armazenamento local)
- Microcharts (exibição de gráficos)
- MVVM (Model-View-ViewModel)

## Instalação

1. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/diario-de-clima.git
    ```

2. Abra o projeto no Visual Studio.

3. Instale as dependências necessárias via NuGet:
    - Microcharts.Forms
    - Newtonsoft.Json (para manipulação de JSON)
    - SQLite.Net-PCL (se usar SQLite para armazenamento)

4. Obtenha uma chave de API gratuita da [OpenWeatherMap](https://openweathermap.org/api) e insira-a no arquivo `WeatherService.cs`.

5. Compile e execute o projeto em um dispositivo ou emulador Android/iOS.

## Estrutura do Projeto
- **Services**: Lida com a requisição de dados da API.
- **Models**: Contém as classes que representam os dados da previsão do tempo.
- **Views**: Páginas de interface, como lista de cidades e detalhes da previsão.
- **ViewModels**: Lógica da interface e comunicação entre a View e os Models.
