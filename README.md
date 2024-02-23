# Evaluación Técnica
## Análisis y discusión

En este apartado se desarrollará un análisis y discusión en lo que corresponde a
porqué se considera o no que la implementación que cumple con la arquitectura limpia y
los principios SOLID.

En una primera instancia, con respecto a los principios SOLID, los cuales se
definen por cada una de las letras de su nombre, se concluye:

- **Single Responsibility Principle:** se cumple debido a la separación de
sus funcionalidades, donde por ejemplo, en la capa de Dominio se dividen las responsabilidades entre
Value Objects, Entities y AggregateRoots. La capa de Infraestructura se adapta con este principio
debido a que divide las responsabilidades entre las distintas configuraciones necesarias para los 
repositorios y las funcionalidades de los repositorios como tal. Con respecto a la
capa de Aplicación, también se realiza la separación de responsabilidades mediante
la utilización de interfaces, las cuales permiten la distinción entre el
establecimiento de las funciones que deben ser implementadas y su implementación
concreta.

- **Open/Closed Principle:** se efectúa debido a cada una de las interfaces que proporciona las
diferentes capas, por ejemplo, podemos observar que en la capa Dominio se tiene una interfaz
para `ValueObject`, `Entity` y `AggregateRoot`, donde no se permite modificar estas, pero las
cuales están abiertas a extensiones. Además este mismo concepto es visible en las interfaces
las cuales proporciona la capa de aplicación.

- **Liskov Substitution Principle:** se cumple con éste debido a que las clases derivadas
se dedican a añadir funcionalidad especializada sobre la base clase, en vez de realizar
cambios sobre el comportamiento de los métodos de las clases base.
Por ejemplo, podemos observar que en la capa Dominio se tienen clases base en su núcleo (`Entity`,
`Value Object`, `AggregateRoot`), las cuales son heredadas por CareerAggregate (`Career`,
`CareerName`, `Content`, `ContentDescription` y `etc`), aumentando sus funcionalidades.

- **Interface Segregation Principle:** este principio se respeta debido a la función que
cumple nuestra capa de aplicación, la cual en sus interfaces únicamente enlista un conjunto
de funcionalidades requeridas y espera que sean provistas por la capa de infraestructura.

- **Dependency Inversion Principle:** este principio es acatado debido a que las distintas
capas del sistema presentan una dependencia de manera que las capas más externas puedan ser
modificadas sin que las capas internas se vean afectadas por éste cambio.

Finalmente, se hablará respecto a los principios pertenecientes a la arquitectura limpia,
es importante mencionar que estos incluyen los principios SOLID ya brevemente explicados en
el apartado superior. La arquitectura limpia implica una estructuración del proyecto mediante
las capas `Domain`, `Application` e `Infrastructure`, en las cuales `Domain` no debe depender
de ninguna, siendo el centro de la inversión de dependencias requerida en los principios SOLID.
Por otro lado, `Application` fue creada de manera que únicamente dependa de la capa `Domain`
para poder generar la interfaz e implementación de los casos de uso del sistema.
Con respecto a `Infrastructure`, esta utiliza referencias a `Domain` y `Application` para que
de esta manera se implementen las tecnologías requeridas por el sistema.

En particular, nuestra solución siguió el mismo diseño propuesto por estos principios,
los cuales junto con el cumplimiento de SOLID, hacen que toda nuestra solución pueda ser
considerada como un sistema que cumple con arquitectura limpia.

## Estados del proyecto

### Estado de funcionalidades requeridas

- **Buscar una carrera por nombre:** Completo
- **Mostrar información de una carrera con su presupuesto de becas:** Completo
- **Mostrar información de una carrera con sus contenidos:** Completo

### Funcionalidad de la Versión

Nos complace informar que la versión actual del proyecto se encuentra completamente funcional y lista para su uso.

## Coevaluación

* **Daniel Pérez Morera:** 50
* **Jean Paul Chacón González:** 50

## Autores

[Daniel Pérez Morera](mailto:daniel.perezmorera@ucr.ac.cr)

[Jean Paul Chacón González](mailto:jean.chacongonzalez@ucr.ac.cr)


