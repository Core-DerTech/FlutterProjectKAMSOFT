import 'package:flutter/material.dart';
import '../models/patient_result.dart';
import '../repositories/MediacalRepository.dart';

class DashboardScreen extends StatefulWidget {
  @override
  _DashboardScreenState createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  final MedicalRepository _repository = MedicalRepository();
  late Future<List<PatientResult>> _resultsFuture;

  @override
  void initState() {
    super.initState();
    _resultsFuture = _repository.fetchDashboardData();
  }

  Color _parseColor(String colorName) {
    switch (colorName) {
      case "Red": return Colors.red.shade400;
      case "Yellow": return Colors.orange.shade400;
      default: return Colors.green.shade400;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Doctor Dashboard")),
      body: FutureBuilder<List<PatientResult>>(
        future: _resultsFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } 
          if (snapshot.hasError) {
            return Center(child: Text("Error: ${snapshot.error}"));
          }
          if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(child: Text("No patient results found."));
          }
          return ListView.builder(
            itemCount: snapshot.data!.length,
            itemBuilder: (context, index) {
              final item = snapshot.data![index];
              return Card(
                elevation: 4,
                margin: EdgeInsets.all(8),
                color: _parseColor(item.statusColor),
                child: ListTile(
                  title: Text(item.patientName, style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white)),
                  subtitle: Text("Result: ${item.formattedValue}", style: TextStyle(color: Colors.white)),
                  trailing: item.isCritical 
                    ? Icon(Icons.warning, color: Colors.white)
                    : null,
                ),
              );
            },
          );
        },
      ),
    );
  }
}