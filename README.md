# How to run

This demo can run as an .NET API or as a .NET Console app, the inputs are basically the same for both modes but the difference comes on the input method and the output channel.

## Run as an API

>$ dotnet run ABCpdfDemo.API.dll

## Run as a Console app

>$ ABCpdfDemo.Console.exe

Input is receiverd from STDIN, so common usage will look like:

>$ cat data.json | ABCpdfDemo.Console.exe

Output PDF will be print out to STDOUT by default, so it can be redirected to another process or file.

>$ cat data.json | ABCpdfDemo.Console.exe > example.pdf

Use parameter `-o` for using the property `outputFileName` found on the json input schema.

## Structure of the JSON input

`
{
  "outputFileName": "Demo.pdf",
  "template": {
    "path":"template.docx",
    "templateType": 3
  },
  "config": null,
  "content":{
    "Name": "Eliezer Pacheco",
    "DateEffective": "08/12/2024"
  },
  "tables": null
}
`

Please define the name of the fields/content controls to populate under the `content` property. Template information should specify absolute path to the template and the type of template that is going to be loaded:

>1 -> PDF
>2 -> HTML
>3 -> Docx

In case that the template type is null it will use PDF rendering as default. In case of using a template type not show on the list above, the application will thrown an `NotSupportedException`