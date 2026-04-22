import 'package:flutter/material.dart';
import '../services/api_service.dart';
import '../models/patient.dart';

class PatientsScreen extends StatefulWidget {
  const PatientsScreen({super.key});

  @override
  State<PatientsScreen> createState() => _PatientsScreenState();
}

class _PatientsScreenState extends State<PatientsScreen> {
  final ApiService _apiService = ApiService();

  List<Patient> _patients = [];
  bool _isLoading = true;
  String? _errorMessage;

  @override
  void initState() {
    super.initState();
    _fetchPatients();
  }

  Future<void> _fetchPatients() async {
    setState(() {
      _isLoading = true;
      _errorMessage = null;
    });

    try {
      final patients = await _apiService.getPatients();
      setState(() {
        _patients = patients;
      });
    } catch (e) {
      setState(() {
        _errorMessage = e.toString();
      });
      print('Error fetching patients: $e');
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Patients'),
      ),
      body: _buildBody(),
    );
  }

  Widget _buildBody() {
    if (_isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (_errorMessage != null) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Error: $_errorMessage'),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: _fetchPatients,
              child: const Text('Retry'),
            ),
          ],
        ),
      );
    }

    if (_patients.isEmpty) {
      return const Center(child: Text('No patients found'));
    }

    return ListView.builder(
      itemCount: _patients.length,
      itemBuilder: (context, index) {
        final patient = _patients[index];
        return ListTile(
          title: Text(patient.name.firstName),
          subtitle: Text('PESEL: ${patient.pessel}'),
        );
      },
    );
  }
}