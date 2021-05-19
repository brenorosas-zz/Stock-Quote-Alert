# Stock Quote Alert
> Descrição curta sobre o que seu projeto faz.

[![NPM Version][npm-image]][npm-url]
[![Build Status][travis-image]][travis-url]
[![Downloads Stats][npm-downloads]][npm-url]

Esse projeto foi feito como parte do processo seletivo de estágio na Inoa Sitemas.

O objetivo desse sistema é auxiliar um investidor nas suas decisões de comprar/vender ativos da B3, monitorando continuamente e avisar, via e-mail, caso a cotação de um caia mais do que um certo nível, ou suba acima de outro.

O programa é uma aplicação de console.

![](/images/header.png)

## Instalação

OS X & Linux:

```sh
npm install my-crazy-module --save
```

Windows:

```sh
edit autoexec.bat
```

## Exemplo de uso

Executando o programa. O mesmo pode ser executado com ativos iniciais para monitoração.

Sendo os parâmetros: Ticker ReferênciaVenda ReferênciaCompra

Exemplo:
```sh
./stock-quote-alert.exe PETR4 22.67 22.59 B3SA3 17.50 17.05
```

Durante a execução temos 3 comandos que podemos executar:

add - Adiciona um ativo ao monitoramento

```sh
add ABEV3 17.78 17.30
```
ls - Lista os ativos em monitoramento mostrando seu Id, Ticker, referência de venda e referência compra.

```sh
ls
```
Caso tenha seguido os comandos iguais os vistos até agora, verá no console essas informações:

![](/images/header.png)

rm - Remove um ativo do monitoramento tomando como referência o Id.

```sh
rm 0
```

Nesse caso removeria o ativo PETR4.

Para ver todos os comandos basta digitar:

```sh
--help
```