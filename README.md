# Encuestas ACME

Este proyecto esta enfocado en crear encuestas en línea de forma dinámica y eficiente, generando un link unico para acceder a la encuesta y llenarla.

Se utilizaron tecnologías como:

- C#
- Asp. NET Core
- EntityFramework
- Automapper
- JWT Authentication 
- Postman
- SQL Server

## Instalación
Al momento de implementar este proyecto a su entorno es conveniente mencionar algunas consideraciones:

**Conexion a Base de Datos:** Este proyecto utiliza SQL server y la creación e implementación de tablas y procedimientos almacenados se realizan por medio de Entityframework, importante modificar la cadena de conexion almacenada en el proveedor de configuración *appsettings.Development.json*

## Intrucciones de Uso
Este proyecto fue desarrollado con la tecnología ASP. NET Core Web API, por lo que no cuenta con interface gráfica, a pesar que cuenta con swager de forma predeterminada para consumir las apis, es recomendado utilizar Postman junto con la collection creada para realizar las practicas con las apis desarrolladas en el proyecto.

**Crear usuario:** El proyecto por medio de una api permite agregar usuarios nuevos a la base de datos, agregar el usuario que permite la validación en las apis está contemplado en la collection de postman.

**Validación de usuario:** Para utilizar las apis que permiten crear, modificar, eliminar y seleccionar los datos de una encuesta es necesario validar el usuario correcto e ingresar el token de JWT.

**Collection de Postman:** Dentro de esta collection adjunta en el repositorio encontrarás todo lo necesario para realizar las practicas con este proyecto  ya sea crear encuestas, crear los campos de las encuestas, ingresar las respuestas, modificar los campos de la encuesta, eliminar la encuesta y ver los datos que se han ingresado en las encuestas por medio de su id.

###### **Kevin Aurelio Alegre Soto 2024.**
