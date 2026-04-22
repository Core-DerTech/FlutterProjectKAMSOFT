import 'package:flutter/material.dart';

class AdapterView extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Adapter View'),
      ),
      body: const Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Adapter View'),
            SizedBox(height: 16),
            Text("To be implemented"),
          ],
        ),
      ),
    );
  }

}

class AdaptModel{
  final String name;
  final String description;

  AdaptModel({required this.name, required this.description});
}