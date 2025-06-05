export default function ModalRtdo({ mostrar, msj }) {
    if (!mostrar) return null;

    return (
        <div className="modal-container">
            <div className="modal-content">
                <h2>Resultado de la Publicación</h2>
                <p>{msj}</p>
                <button onClick={() => window.location.reload()} className="btn-close">
                    Cerrar
                </button>
            </div>
        </div>
    );
}