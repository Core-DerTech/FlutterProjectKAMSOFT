import 'package:flutter/material.dart';

class WidgetTree extends StatelessWidget {
  const WidgetTree({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Widget Tree')),
      body:  Padding(padding: const EdgeInsets.all(16.0), child: Column(
        children: [
          const Text("Widget tree"),
          const SizedBox(height: 16),
          Container(
            color: Colors.blue,
            padding: const EdgeInsets.all(16.0),
            child: Column(
              children: [
                const Text('Container'),
                const SizedBox(height: 16),
                Row(
                  children: [
                    const Text('Row'),
                    const SizedBox(width: 16),
                    Container(
                      color: Colors.red,
                      padding: const EdgeInsets.all(16.0),
                      child: const Text('Nested Container'),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      )),
    );
  }
}