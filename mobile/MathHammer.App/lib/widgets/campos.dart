import 'package:flutter/material.dart';

import '../temas/tema.dart';

/// Etiqueta en mayúsculas de un campo.
class EtiquetaCampo extends StatelessWidget {
  const EtiquetaCampo(this.texto, {super.key});

  final String texto;

  @override
  Widget build(BuildContext context) {
    return Text(
      texto.toUpperCase(),
      style: inter(tamano: 12, color: textoSuave, espaciado: 0.8),
    );
  }
}

/// Campo de texto con estilo grimdark.
class CampoTexto extends StatelessWidget {
  const CampoTexto({
    super.key,
    required this.etiqueta,
    required this.controlador,
    this.tipo = const TextInputType.numberWithOptions(signed: true),
  });

  final String etiqueta;
  final TextEditingController controlador;
  final TextInputType tipo;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        EtiquetaCampo(etiqueta),
        const SizedBox(height: 6),
        TextField(
          controller: controlador,
          keyboardType: tipo,
          style: inter(tamano: 15, color: textoMedio),
          decoration: _decoracion(tema),
        ),
      ],
    );
  }
}

/// Selector desplegable con opción vacía "—".
class CampoSelector extends StatelessWidget {
  const CampoSelector({
    super.key,
    required this.etiqueta,
    required this.valor,
    required this.opciones,
    required this.alCambiar,
  });

  final String etiqueta;
  final int? valor;
  final List<int> opciones;
  final ValueChanged<int?> alCambiar;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        EtiquetaCampo(etiqueta),
        const SizedBox(height: 6),
        DropdownButtonFormField<int?>(
          initialValue: valor,
          isExpanded: true,
          dropdownColor: const Color(0xFF15181a),
          style: inter(tamano: 15, color: textoMedio),
          decoration: _decoracion(tema),
          items: [
            DropdownMenuItem<int?>(
              value: null,
              child: Text('—', style: inter(tamano: 15, color: textoTenue)),
            ),
            for (final opcion in opciones)
              DropdownMenuItem<int?>(
                value: opcion,
                child: Text('$opcion+', style: inter(tamano: 15, color: textoMedio)),
              ),
          ],
          onChanged: alCambiar,
        ),
      ],
    );
  }
}

/// Habilidad con casilla personalizada y selector opcional.
class Habilidad extends StatelessWidget {
  const Habilidad({
    super.key,
    required this.texto,
    required this.activa,
    required this.alCambiar,
    this.selector = false,
    this.valorSelector,
    this.alCambiarSelector,
    this.opcionesSelector = const [1, 2, 3],
  });

  final String texto;
  final bool activa;
  final ValueChanged<bool> alCambiar;
  final bool selector;
  final int? valorSelector;
  final ValueChanged<int>? alCambiarSelector;
  final List<int> opcionesSelector;

  @override
  Widget build(BuildContext context) {
    final tema = TemaAlcance.de(context);
    return InkWell(
      onTap: () => alCambiar(!activa),
      child: Row(
        children: [
          _marca(tema),
          const SizedBox(width: 10),
          Expanded(
            child: Text(
              texto,
              style: inter(
                tamano: 13,
                peso: 500,
                color: activa ? tema.acentoFuerte : textoSuave,
                espaciado: 0.6,
              ),
            ),
          ),
          if (selector && activa && valorSelector != null && alCambiarSelector != null)
            DropdownButton<int>(
              value: valorSelector,
              isDense: true,
              underline: const SizedBox.shrink(),
              dropdownColor: const Color(0xFF15181a),
              style: inter(tamano: 13, color: tema.acentoClaro),
              items: [
                for (final opcion in opcionesSelector)
                  DropdownMenuItem<int>(value: opcion, child: Text('$opcion')),
              ],
              onChanged: (nuevo) {
                if (nuevo != null) {
                  alCambiarSelector!(nuevo);
                }
              },
            ),
        ],
      ),
    );
  }

  Widget _marca(TemaMathHammer tema) {
    return Container(
      width: 14,
      height: 14,
      decoration: BoxDecoration(
        color: activa ? tema.acentoMarca : const Color(0xFF121516),
        border: Border.all(color: activa ? tema.acentoFuerte : const Color(0xFF626768)),
      ),
      child: activa
          ? Container(
              margin: const EdgeInsets.all(3),
              color: const Color(0xFF191b1d),
            )
          : null,
    );
  }
}

InputDecoration _decoracion(TemaMathHammer tema) {
  return InputDecoration(
    isDense: true,
    filled: true,
    fillColor: panelOscuro,
    contentPadding: const EdgeInsets.symmetric(horizontal: 11, vertical: 12),
    enabledBorder: const OutlineInputBorder(
      borderRadius: BorderRadius.zero,
      borderSide: BorderSide(color: bordeCampo),
    ),
    focusedBorder: OutlineInputBorder(
      borderRadius: BorderRadius.zero,
      borderSide: BorderSide(color: tema.acento),
    ),
  );
}