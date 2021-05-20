# Stock Quote Alert

Esse projeto foi feito como parte do processo seletivo de estágio na Inoa Sitemas.

O objetivo desse sistema é auxiliar um investidor nas suas decisões de comprar/vender ativos da B3, monitorando continuamente e avisar, via e-mail, caso a cotação de um caia mais do que um certo nível, ou suba acima de outro.

O programa é uma aplicação de console.

## Instalação

Clone esse repositório para seu desktop.

Linux:

Siga o tutorial de instalação referente a sua distribuição no site:

[Download .NET SDK 5.0 Linux](https://docs.microsoft.com/pt-br/dotnet/core/install/linux).

Windows:

Faça o download do .NET SDK 5.0 clicando no link:

[Download .NET SDK 5.0 Windows](https://dotnet.microsoft.com/download).

Siga os passos de instalação no próprio site de download.

## Arquivo de configuração

Use o .env.example para criar um novo arquivo .env com as informações:

```sh
EMAIL_ADDRESS="Email para envio dos alertas"
EMAIL_PASSWORD="Senha do email EMAIL_ADDRESS"
DESTINATION_EMAIL="Email destino dos alertas"
SMTP_HOST="Host do SMTP Client"
SMTP_PORT="Port do SMTP CLient"
```

Se seu email tiver identificação por dois fatores, use um [app password](https://support.google.com/accounts/answer/185833).

Possivelmente será preciso alterar as permissões no seu gmail para permitir acesso de aplicativos com menor segurança.

## Exemplo de uso

Executando o programa. O mesmo pode ser executado com ativos iniciais para monitoração.

Sendo os parâmetros: Ticker ReferênciaVenda ReferênciaCompra

Exemplo:

No diretório Stock-Quote-Alert.

```sh
dotnet run PETR4 22.67 22.59 B3SA3 17.50 17.05
```

Durante a execução temos 3 comandos que podemos executar:

add - Adiciona um ativo ao monitoramento

```sh
add ABEV3 17.78 17.30
```

ls - Lista os ativos em monitoramento mostrando seu Id, Ticker, referência para venda, referência para compra e o estado.

```sh
ls
```

Caso tenha seguido os comandos iguais os vistos até agora, verá no console essas informações:

![](/images/list.png)

rm - Remove um ativo do monitoramento tomando como referência o Id.

```sh
rm 0
```

Nesse caso removeria o ativo PETR4.

Para ver todos os comandos basta digitar:

```sh
--help
```
