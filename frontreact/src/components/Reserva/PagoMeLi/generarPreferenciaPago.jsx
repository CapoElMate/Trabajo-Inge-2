export async function generarPreferenciaPago(nombreProducto,precio,idPublicacion) {
    try {
        const request = {
            titulo: nombreProducto,
            cantidad: 1,
            precio: precio,
            backUrlSuccess: `https://localhost:5173/DetallePublicacion/${idPublicacion}/success`,
            backUrlFailure: `https://localhost:5173/DetallePublicacion/${idPublicacion}/failure`,
            backUrlPending: `https://localhost:5173/DetallePublicacion/${idPublicacion}/pending`,
            idProducto: idPublicacion
        }

        const response = await fetch("http://localhost:5000/api/PagoMeLi/generarPreferenciaDePago", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(request),
        });

        if (!response.ok) {
            throw new Error("Error al generar la preferencia de pago");
        }

        const data = await response.json();
        return data.id; // Asegúrate que tu backend devuelve { id: ... }
    } catch (error) {
        console.error("Error al generar la preferencia de pago:", error);
        throw error;
    }
}