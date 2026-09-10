import 'package:flutter/material.dart';

import '../temas/tema.dart';

/// Selector desplegable de temas de color.
class SelectorTema extends StatelessWidget {
  const SelectorTema({super.key, required this.tema, required this.alCambiarTema});

  final TemaMathHammer tema;
  final ValueChanged<NombreTema> alCambiarTema;

  @override
  Widget build(BuildContext context) {
    return PopupMenuButton<NombreTema>(
      tooltip: '',
      color: const Color(0xFF15181a),
      position: PopupMenuPosition.under,
      onSelected: alCambiarTema,
      itemBuilder: (context) => [
        for (final opcion in listaTemas)
          PopupMenuItem<NombreTema>(
            value: opcion.id,
            child: Row(
              children: [
                _circulo(opcion.colorMuestra),
                const SizedBox(width: 10),
                Text(
                  opcion.etiqueta,
                  style: inter(
                    tamano: 12,
                    peso: opcion.id == tema.id ? 700 : 500,
                    color: opcion.id == tema.id ? tema.acentoClaro : textoClaro,
                    espaciado: 1,
                  ),
                ),
              ],
            ),
          ),
      ],
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
        decoration: BoxDecoration(
          color: panelOscuro,
          border: Border.all(color: tema.acento),
        ),
        child: Row(
          mainAxisSize: MainAxisSize.min,
          children: [
            _circulo(tema.colorMuestra),
            const SizedBox(width: 9),
            Text(tema.etiqueta, style: inter(tamano: 11, peso: 600, color: textoClaro, espaciado: 1)),
            const SizedBox(width: 6),
            Text('▾', style: inter(tamano: 12, color: tema.acentoFuerte)),
          ],
        ),
      ),
    );
  }

  Widget _circulo(Color color) {
    return Container(
      width: 12,
      height: 12,
      decoration: BoxDecoration(
        color: color,
        shape: BoxShape.circle,
        border: Border.all(color: Colors.white24),
      ),
    );
  }
}