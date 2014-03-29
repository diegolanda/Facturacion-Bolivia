Facturacion-Bolivia
===================
**Generacion del Codigo de Control V7**

El imprimir facturas es un problema de todos los que programamos sistemas en Bolivia,
desde el simple hecho de que Impuestos Nacionales no tiene bien claro los procedimientos hasta que el departamento
de Sistemas del SIN no hace mas que complicarnos la vida.

La idea de este proyecto es el de poder contar con una libreria que podamos usar para 
generar lo necesario para la impresion de Facturas.

Para poder generar el Codigo de Control necesitas realizar lo siguiente:

C#
--
<code>
CodigoDeControl.Instancia.Generar(string autorizacion, string numero, string nitci, string fecha, string monto, string llave);

//Ejm

CodigoDeControl.Instancia.Generar("7904006306693", "876814", "1665979", "20080519", "35958.60", @"zZ7Z]xssKqkEf_6K9uH(EcV+%x+u[Cca9T%+_$kiLjT8(zr3T9b5Fx2xG-D+_EBS");
</code>

Aportes
=======

Si deseas aportar al proyecto simplemente haz y pull request, procura ejecutar los Unit Tests antes

Te Gusto?
=========

Si te gusto, invitame una Pizza!

email: diegolanda@msn.com
fb: [facebook](https://www.facebook.com/diego.landa.bo "Diego Landa")