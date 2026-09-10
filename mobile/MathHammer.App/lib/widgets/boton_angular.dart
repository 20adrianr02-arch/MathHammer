import 'package:flutter/material.dart';

import '../temas/tema.dart';
import 'panel_marco.dart';

/// Botón principal con esquinas recortadas (como el de la web).
class BotonAngular extends StatelessWidget {
  const BotonAngular({
    super.key,
    required this.texto,
    required this.alPulsar,
    this.deshabilitado = false,
  });

  final String texto;
  final VoidCallback alPulsar;
  final bool deshabilitado;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return ClipPath(
      clipper: const RecorteEsquinas(corte: 8),
      child: Material(
        color: deshabilitado ? tema.acentoOscuro.withValues(alpha: 0.6) : tema.acentoOscuro,
        child: InkWell(
          onTap: deshabilitado ? null : alPulsar,
          child: Container(
            constraints: const BoxConstraints(minWidth: 260),
            padding: const EdgeInsets.symmetric(vertical: 16, horizontal: 29),
            alignment: Alignment.center,
            decoration: BoxDecoration(
              border: Border.all(color: tema.acentoClaro),
            ),
            child: Text(
              texto,
              textAlign: TextAlign.center,
              style: inter(tamano: 16, peso: 700, color: Colors.white, espaciado: 1.6),
            ),
          ),
        ),
      ),
    );
  }
}