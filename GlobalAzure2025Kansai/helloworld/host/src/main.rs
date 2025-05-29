use hyperlight_host::func::{ParameterValue, ReturnType};
use hyperlight_host::sandbox_state::sandbox::EvolvableSandbox;
use hyperlight_host::sandbox_state::transition::Noop;
use hyperlight_host::{MultiUseSandbox, UninitializedSandbox};

fn main() -> hyperlight_host::Result<()> {
    // Create an uninitialized sandbox with a guest binary
    let mut uninitialized_sandbox = UninitializedSandbox::new(
        hyperlight_host::GuestBinary::FilePath("helloworld-guest".to_string()),
        None
    )?;

    // Initialize sandbox to be able to call host functions
    let mut multi_use_sandbox: MultiUseSandbox = uninitialized_sandbox.evolve(Noop::default())?;

    // Call a function in the guest
    let message = "Hello, Global Azure 2025! I am executing inside of a VM :)\n".to_string();
    // in order to call a function it first must be defined in the guest and exposed so that 
    // the host can call it
    let result = multi_use_sandbox.call_guest_function_by_name(
        "PrintOutput",
        ReturnType::Int,
        Some(vec![ParameterValue::String(message.clone())]),
    );

    assert!(result.is_ok());

    Ok(())
}
