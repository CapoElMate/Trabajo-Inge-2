export async function generarPreferenciaPago(
  nombreProducto,
  reserva
) {
  try {
    const baseUrl = `https://localhost:5001/mapeo?idPublicacion=${reserva.idPublicacion}&state=success`;

    // Crear los parámetros adicionales
    const extraParams = new URLSearchParams({
      fecInicio: reserva.fecInicio,
      fecFin: reserva.fecFin,
      status: reserva.status,
      calle: reserva.calle,
      altura: reserva.altura,
      dpto: reserva.dpto,
      piso: reserva.piso,
      entrega: reserva.tipoEntrega.entrega,
      idAlquiler: reserva.idAlquiler, // se convierte en "null"
      dniCliente: reserva.dniCliente,
      montoTotal: reserva.montoTotal,
    });

    // Concatenar a la URL
    const finalUrl = `${baseUrl}&${extraParams.toString()}`;

    const request = {
      titulo: nombreProducto,
      cantidad: 1,
      precio: reserva.montoTotal,
      backUrlSuccess: finalUrl + `/success`,
      backUrlFailure: `https://localhost:5001/mapeo?idPublicacion=${reserva.idPublicacion}&state=failure`,
      backUrlPending: `https://localhost:5001/mapeo?idPublicacion=${reserva.idPublicacion}&state=pending`,
      idProducto: reserva.idPublicacion.toString(),
    };

    const response = await fetch(
      "http://localhost:5000/api/PagoMeLi/generarPreferenciaDePago",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
      }
    );

    if (!response.ok) {
      throw new Error("Error al generar la preferencia de pago");
    }

    const data = await response.json();
    return data.id; // Aseg�rate que tu backend devuelve { id: ... }
  } catch (error) {
    console.error("Error al generar la preferencia de pago:", error);
    throw error;
  }
}
